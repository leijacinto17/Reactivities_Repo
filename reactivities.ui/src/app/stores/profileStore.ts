import { makeAutoObservable, reaction, runInAction } from "mobx";
import agent from "../api/agent";
import { Photo, Profiles, UserActivity } from "../models/profiles";
import { UserAboutValues } from "../models/user";
import { store } from "./store";

export default class ProfileStore {
  profile: Profiles | null = null;
  loadingProfile = false;
  uploading = false;
  loadingSetMainPhoto = false;
  isSubmittingAbout = false;
  followings: Profiles[] = [];
  loadingFollow = false;
  loadingFollowing = false;
  activeTab = 0;
  userActivities: UserActivity[] = [];
  loadingActivities = false;

  constructor() {
    makeAutoObservable(this);

    reaction(
      () => this.activeTab,
      (activeTab) => {
        if (activeTab === 3 || activeTab === 4) {
          const predicate = activeTab === 3 ? "followers" : "following";
          this.loadFollowings(predicate);
        } else {
          this.followings = [];
        }
      }
    );
  }

  setActiveTab = (activeTab: any) => {
    this.activeTab = activeTab;
  };

  get isCurrentUser() {
    if (store.userStore.user && this.profile) {
      return store.userStore.user.username === this.profile.username;
    }
    return false;
  }

  loadProfile = async (username: string) => {
    this.loadingProfile = true;
    try {
      const profile = await agent.Profile.get(username);

      runInAction(() => {
        this.profile = profile;
        this.loadingProfile = false;
      });
    } catch (error) {
      console.log(error);
      runInAction(() => {
        this.loadingProfile = false;
      });
    }
  };

  uploadPhoto = async (file: Blob) => {
    this.uploading = true;
    try {
      const response = await agent.Profile.uploadPhoto(file);
      const photo = response.data;
      runInAction(() => {
        if (this.profile) {
          this.profile.photos?.push(photo);
          if (photo.isMain && store.userStore.user) {
            store.userStore.setImage(photo.url);
          }
        }
        this.uploading = false;
      });
    } catch (error) {
      console.log(error);
      runInAction(() => {
        this.uploading = false;
      });
    }
  };

  setMainPhoto = async (photo: Photo) => {
    this.loadingSetMainPhoto = true;
    try {
      await agent.Profile.setMainPhoto(photo.publicId);
      store.userStore.setImage(photo.url);
      runInAction(() => {
        if (this.profile && this.profile.photos) {
          this.profile.photos.find((a) => a.isMain)!.isMain = false;
          this.profile.photos.find(
            (a) => a.publicId === photo.publicId
          )!.isMain = true;
          this.profile.image = photo.url;
          this.loadingSetMainPhoto = false;
        }
      });
    } catch (error) {
      console.log(error);
      runInAction(() => (this.loadingSetMainPhoto = false));
    }
  };

  deletePhoto = async (photo: Photo) => {
    this.loadingSetMainPhoto = true;
    try {
      await agent.Profile.deletePhoto(photo.publicId);
      runInAction(() => {
        this.profile!.photos = this.profile!.photos!.filter(
          (a) => a.publicId !== photo.publicId
        );
        this.loadingSetMainPhoto = false;
      });
    } catch (error) {
      console.log(error);
      runInAction(() => (this.loadingSetMainPhoto = false));
    }
  };

  editAbout = async (username: string, profile: UserAboutValues) => {
    this.isSubmittingAbout = true;
    try {
      await agent.Profile.updateAbout(username, profile);
      runInAction(() => {
        this.profile!.displayName = profile.displayName!;
        store.userStore.setDisplayName(profile.displayName!);
        this.profile!.bio = profile.bio!;
        this.profile = { ...this.profile, ...(profile as Profiles) };
        store.activityStore.setLoadingInitial(true);
        this.isSubmittingAbout = false;
      });
    } catch (error) {
      console.log(error);
      runInAction(() => (this.isSubmittingAbout = false));
    }
  };

  updateFollowing = async (username: string, following: boolean) => {
    this.loadingFollow = true;
    try {
      await agent.Profile.updateFollowing(username);
      store.activityStore.updateAttendeeFollowing(username);
      runInAction(() => {
        if (
          this.profile &&
          this.profile.username !== store.userStore.user?.username &&
          this.profile.username === username
        ) {
          following
            ? this.profile.followersCount++
            : this.profile.followersCount--;
          this.profile.following = !this.profile.following;
        }

        if (
          this.profile &&
          this.profile.username === store.userStore.user?.username
        ) {
          following
            ? this.profile.followingCount++
            : this.profile.followingCount--;
        }

        this.followings.forEach((profile) => {
          if (profile.username === username) {
            profile.following
              ? profile.followersCount--
              : profile.followersCount++;
            profile.following = !profile.following;
          }
        });
        this.loadingFollow = false;
      });
    } catch (error) {
      console.log(error);
      runInAction(() => {
        this.loadingFollow = false;
      });
    }
  };

  loadFollowings = async (predicate: string) => {
    this.loadingFollowing = true;
    try {
      const followings = await agent.Profile.listFollowings(
        this.profile!.username,
        predicate
      );
      runInAction(() => {
        this.followings = followings;
        this.loadingFollowing = false;
      });
    } catch (error) {
      console.log(error);
      runInAction(() => {
        this.loadingFollowing = false;
      });
    }
  };

  loadUserActivities = async (username: string, predicate?: string) => {
    this.loadingActivities = true;
    try {
      const activities = await agent.Profile.listOfUseActivities(
        username,
        predicate!
      );
      runInAction(() => {
        this.userActivities = activities;
        this.loadingActivities = false;
      });
    } catch (error) {
      console.log(error);
      runInAction(() => {
        this.loadingActivities = false;
      });
    }
  };
}
