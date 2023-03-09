import { Form, Formik } from "formik";
import { observer } from "mobx-react-lite";
import { Button } from "semantic-ui-react";
import MyTextInput from "../../app/common/form/MyTextInput";
import { Profiles } from "../../app/models/profiles";
import * as Yup from "yup";
import { UserAboutValues } from "../../app/models/user";
import { useStore } from "../../app/stores/store";
import MyTextArea from "../../app/common/form/MyTextArea";

interface Props {
  profile: Profiles;
  setEditMode: (editMode: boolean) => void;
}

export default observer(function ProfileForm({ profile, setEditMode }: Props) {
  const {
    profileStore: { editAbout },
  } = useStore();

  const validationSchema = Yup.object({
    displayName: Yup.string().required("The Display Name is required"),
  });

  function handleFormSubmit(profile: Profiles) {
    if (!profile) return;
    const message: UserAboutValues = {
      displayName: profile.displayName,
      bio: profile.bio,
    };

    editAbout(profile.username, message).then(() => {
      setEditMode(false);
    });
  }

  return (
    <Formik
      initialValues={profile}
      validationSchema={validationSchema}
      onSubmit={(values) => handleFormSubmit(values)}
    >
      {({ handleSubmit, isValid, isSubmitting, dirty }) => (
        <Form className="ui form" onSubmit={handleSubmit} autoComplete="off">
          <MyTextInput name="displayName" placeholder="Display Name" />
          <MyTextArea rows={3} name="bio" placeholder="Add your bio" />
          <Button
            disabled={isSubmitting || !dirty || !isValid}
            loading={isSubmitting}
            floated="right"
            positive
            type="submit"
            content="Submit"
            key="content"
          />
        </Form>
      )}
    </Formik>
  );
});
