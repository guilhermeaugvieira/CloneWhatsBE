namespace CloneWhatsBE;

public static class UserFakeDb
{
    public static readonly List<User> Users = [
        new User(new Guid("4A9DA15B-4D93-4080-AC3A-35A0CF30B5FE"), "Harry Potter"),
        new User(new Guid("176822EC-138D-46E0-AA29-F5EB772E476D"), "Albus Dumbledore"),
        new User(new Guid("8036918B-35B2-44EE-A0A7-81E5D3898CDE"), "Lord Valdemort")
    ];
}
