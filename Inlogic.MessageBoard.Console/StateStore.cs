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

    value.Add(new Message(userName, content, TimeOnly.FromDateTime(DateTime.Now)));
  }

  public List<Message> ReadProjectMessages(string projectName)
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
      value = ([]);
      _userFollowings[userName] = value;
    }
    if (!value.Contains(projectName))
    {
      value.Add(projectName);
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

    return [.. wallMessages.OrderByDescending(m => m.Timestamp)];
  }
}

public class Message(string userName, string content, TimeOnly timestamp)
{
  public string UserName { get; } = userName;
  public string Content { get; } = content;
  public TimeOnly Timestamp { get; } = timestamp;
}
