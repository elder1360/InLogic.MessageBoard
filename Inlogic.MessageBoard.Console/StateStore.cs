namespace Inlogic.MessageBoard.Console;

public class InMemoryStateStore : IInMemoryStateStore
{
  private readonly Dictionary<string, List<Message>> _projectMessages;
  private readonly Dictionary<string, List<string>> _userFollowings;

  public InMemoryStateStore()
  {
    _projectMessages = [];
    _userFollowings = [];
  }

  public void PostMessage(string userName, string projectName, string content)
  {
    if (!_projectMessages.TryGetValue(projectName, out var value))
    {
      value = [];
      _projectMessages[projectName] = value;
    }

    value.Add(new Message(userName, content, DateTime.Now));
  }

  public IEnumerable<Message> ReadProjectMessages(string projectName)
  {
    if (_projectMessages.TryGetValue(projectName, out var messages))
    {
      return messages;
    }
    return [];
  }

  public void FollowProject(string userName, string projectName)
  {
    if (!_userFollowings.TryGetValue(userName, out var value))
    {
      value = [];
      _userFollowings[userName] = value;
    }
    if (!value.Contains(projectName))
    {
      value.Add(projectName);
    }
  }

  public Dictionary<string, IEnumerable<Message>> GetUserWall(string userName)
  {
    Dictionary<string, IEnumerable<Message>> wall = [];
    if (_userFollowings.TryGetValue(userName, out var projects))
    {
      foreach (var project in projects)
      {
        wall.Add(project, ReadProjectMessages(project));
      }
    }
    return wall;
  }
}

public class Message(string userName, string content, DateTime timestamp)
{
  public string UserName { get; } = userName;
  public string Content { get; } = content;
  public DateTime Timestamp { get; } = timestamp;
}
