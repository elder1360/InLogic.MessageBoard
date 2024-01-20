namespace Inlogic.MessageBoard.Console;

public interface IInMemoryStateStore
{
  void FollowProject(string userName, string projectName);
  List<Message> GetUserWall(string userName);
  void PostMessage(string userName, string projectName, string content);
  List<Message> ReadProjectMessages(string projectName);
}
