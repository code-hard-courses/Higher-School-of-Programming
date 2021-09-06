public class KafkaApiDetails implements ApiDetails {

    private String topics;
    private String groupId;

    private KafkaApiDetails(String topics, String groupId) {
        this.topics = topics;
        this.groupId = groupId;
    }

    public static KafkaApiDetails fromTopicsAndGroupId(String topics, String groupId) {
        return new KafkaApiDetails(topics, groupId);
    }
}


public class PersonInfo {

    private String firstName;
    private String lastName;
    private String middleName;
    private String birthday;
    private String gender;

    private PersonInfo(String firstName, String lastName, String middleName) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.middleName = middleName;
    }

    public static PersonInfo fromFullName(String fullName) {
        String[] splittedName = fullName.split(" ");
        return new PersonInfo(splittedName[1], splittedName[0], splittedName[2]);
    }
}

public class Avatar {

    private long id;
    private String photoUrl;

    private Avatar(String photoUrl) {
        this.photoUrl = photoUrl;
    }

    public static Avatar fromPhotoUrl(String photoUrl) {
        return new Avatar(photoUrl);
    }
}