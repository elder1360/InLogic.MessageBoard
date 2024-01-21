using Inlogic.MessageBoard.Console;
using Inlogic.MessageBoard.Console.CommandQuery.Commands;
using Inlogic.MessageBoard.Console.CommandQuery.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(Program).Assembly));
services.AddSingleton<IInMemoryStateStore, InMemoryStateStore>();
var serviceProvider = services.BuildServiceProvider();

var mediator = serviceProvider.GetRequiredService<IMediator>();

await RunInteractiveConsoleAsync(mediator);

static async Task RunInteractiveConsoleAsync(IMediator mediator)
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
      await ProcessAsync(input, mediator);
    }
  }
}

static async Task ProcessAsync(string input, IMediator mediator)
{
  var parts = input.Split(' ');

  if (parts.Length > 2 && parts[1] == "->")
  {
    var userName = parts[0];
    var projectName = parts[2].TrimStart('@');
    var message = string.Join(' ', parts.Skip(3));
    await mediator.Send(new PostMessageCommand(userName, projectName, message));
  }
  else if (parts.Length > 2 && parts[1] == "follows")
  {
    var userName = parts[0];
    var projectName = parts[2].TrimStart('@');
    await mediator.Send(new FollowProjectCommand(userName, projectName));
  }
  else if (parts.Length == 2 && parts[1] == "wall")
  {
    var userName = parts[0];
    await mediator.Send(new DisplayWallQuery(userName));
  }
  else
  {
    var projectName = parts[0];
    await mediator.Send(new ReadProjectQuery(projectName));
  }
}
