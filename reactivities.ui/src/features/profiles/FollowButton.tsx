import { observer } from "mobx-react-lite";
import { SyntheticEvent } from "react";
import { Button, Reveal } from "semantic-ui-react";
import { Profiles } from "../../app/models/profiles";
import { useStore } from "../../app/stores/store";

interface Props {
  profile: Profiles;
}

export default observer(function FollowButton({ profile }: Props) {
  const { profileStore, userStore } = useStore();
  const { updateFollowing, loadingFollow } = profileStore;

  if (userStore.user?.username === profile.username) return null;

  function handleFollow(e: SyntheticEvent, username: string) {
    e.preventDefault();
    profile.following
      ? updateFollowing(username, false)
      : updateFollowing(username, true);
  }

  return (
    <Reveal animated="move">
      <Reveal.Content visible style={{ width: "100%" }}>
        <Button
          fluid
          color="teal"
          content={profile.following ? "Following" : "Not Following"}
        />
      </Reveal.Content>
      <Reveal.Content hidden style={{ width: "100%" }}>
        <Button
          basic
          fluid
          color={profile.following ? "red" : "green"}
          content={profile.following ? "Unfollow" : "Follow"}
          loading={loadingFollow}
          onClick={(e) => handleFollow(e, profile.username)}
        />
      </Reveal.Content>
    </Reveal>
  );
});
