import { User } from "./user";

export interface Profiles {
  username: string;
  displayName: string;
  image?: string;
  bio?: string;
  followersCount: number,
  followingCount: number,
  following: boolean,
  photos: Photo[] | null;
}

export class Profiles implements Profiles {
  constructor(user: User) {
    this.username = user.username;
    this.displayName = user.displayName;
    this.image = user.image;
  }
}

export interface Photo {
  id: number;
  publicId: string;
  url: string;
  isMain: boolean;
}
