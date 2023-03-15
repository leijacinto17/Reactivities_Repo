import { observer } from "mobx-react-lite";
import { Link } from "react-router-dom";
import { Card, Icon, Image } from "semantic-ui-react";
import { Profiles } from "../../app/models/profiles";
import FollowButton from "./FollowButton";

interface Props {
  profile: Profiles;
}

export default observer(function ProfileCard({ profile }: Props) {
  function truncateString(str: string | "", num: number) {
    if (str.length <= num) {
      return str;
    }
    return str.slice(0, num) + "...";
  }

  return (
    <Card as={Link} to={`/profiles/${profile.username}`}>
      <Image src={profile.image || "/assets/user.png"} />
      <Card.Content>
        <Card.Header>{profile.displayName}</Card.Header>
        <Card.Description>
          {truncateString(profile.bio ?? "", 40)}
        </Card.Description>
      </Card.Content>
      <Card.Content extra>
        <Icon name="user" />
        {profile.followersCount}{" "}
        {profile.followersCount > 1 ? "follower" : "followers"}
      </Card.Content>
      <FollowButton profile={profile} />
    </Card>
  );
});
