
namespace Inlogic.MessageBoard.Console;

public interface IInMemoryStateStore
{
  void FollowProject(string userName, string projectName);
  Dictionary<string, IEnumerable<Message>> GetUserWall(string userName);
  void PostMessage(string userName, string projectName, string content);
  IEnumerable<Message> ReadProjectMessages(string projectName);
}
