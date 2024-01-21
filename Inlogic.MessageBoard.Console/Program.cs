using Inlogic.MessageBoard.Console;
using Inlogic.MessageBoard.Console.CommandQuery.Commands;
using Inlogic.MessageBoard.Console.CommandQuery.Queries;
using Inlogic.MessageBoard.Console.Requests;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(Program).Assembly));
services.AddSingleton<IInMemoryStateStore, InMemoryStateStore>();
var serviceProvider = services.BuildServiceProvider();

var mediator = serviceProvider.GetRequiredService<IMediator>();

RunInteractiveConsole(mediator);

static void RunInteractiveConsole(IMediator mediator)
{
  while (true)
  {
    Console.Write("Enter command: ");
    var input = Console.ReadLine();
    if (input?.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase) == true)
    {
      break;
    }

    if (!string.IsNullOrEmpty(input))
    {
      Process(input, mediator);
    }
  }
}

static void Process(string input, IMediator mediator)
{
  var parts = input.Split(' ');

  if (parts.Length > 2 && parts[1] == "->")
  {
    var userName = parts[0];
    var projectName = parts[2].TrimStart('@');
    var message = string.Join(' ', parts.Skip(3));
    mediator.Send(new PostMessageCommand(userName, projectName, message)).Wait();
  }
  else if (parts.Length > 2 && parts[1] == "follows")
  {
    var userName = parts[0];
    var projectName = parts[2].TrimStart('@');
    mediator.Send(new FollowProjectCommand(userName, projectName)).Wait();
  }
  else if (parts.Length == 2 && parts[1] == "wall")
  {
    var userName = parts[0];
    mediator.Send(new DisplayWallQuery(userName)).Wait();
  }
  else
  {
    var projectName = parts[0];
    mediator.Send(new ReadProjectQuery(projectName)).Wait();
  }
}
