public class NetworkText
{
    public string SetNetworkView(StudentModel studentModel, NetworkStatus status)
    {
        return string.Format("{0}, {1} {2}. : {3}",
            studentModel.Lastname,
            studentModel.Givenname,
            studentModel.Middlename,
            new Network().NetWorkStatus(status));
    }

    public string SetNetworkViewActivity(StudentModel studentModel, string activity)
    {
        return string.Format("{0}, {1} {2}, : {3} - {4}",
            studentModel.Lastname,
            studentModel.Givenname,
            studentModel.Middlename,
            new Network().NetWorkStatus(NetworkStatus.Online),
            activity);
    }
}