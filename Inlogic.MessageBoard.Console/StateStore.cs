namespace Inlogic.MessageBoard.Console;

public class InMemoryStateStore : IInMemoryStateStore
{
  private readonly Dictionary<string, List<Message>> _projectMessages;
  private readonly Dictionary<string, List<string>> _userFollowings;

  public InMemoryStateStore()
  {
    _projectMessages = new Dictionary<string, List<Message>>();
    _userFollowings = new Dictionary<string, List<string>>();
  }

  public void PostMessage(string userName, string projectName, string content)
  {
    if (!_projectMessages.ContainsKey(projectName))
    {
      _projectMessages[projectName] = new List<Message>();
    }
    _projectMessages[projectName].Add(new Message(userName, content, DateTime.UtcNow));
  }

  public List<Message> ReadProjectMessages(string projectName)
  {
    if (_projectMessages.TryGetValue(projectName, out var messages))
    {
      return messages;
    }
    return new List<Message>();
  }

  public void FollowProject(string userName, string projectName)
  {
    if (!_userFollowings.ContainsKey(userName))
    {
      _userFollowings[userName] = new List<string>();
    }
    if (!_userFollowings[userName].Contains(projectName))
    {
      _userFollowings[userName].Add(projectName);
    }
  }

  public List<Message> GetUserWall(string userName)
  {
    var wallMessages = new List<Message>();
    if (_userFollowings.TryGetValue(userName, out var projects))
    {
      foreach (var project in projects)
      {
        wallMessages.AddRange(ReadProjectMessages(project));
      }
    }
    return wallMessages.OrderByDescending(m => m.Timestamp).ToList();
  }
}

public class Message
{
  public string UserName { get; }
  public string Content { get; }
  public DateTime Timestamp { get; }

  public Message(string userName, string content, DateTime timestamp)
  {
    UserName = userName;
    Content = content;
    Timestamp = timestamp;
  }
}
