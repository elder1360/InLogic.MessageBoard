using MediatR;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(Program).Assembly));

var serviceProvider = services.BuildServiceProvider();

var mediator = serviceProvider.GetRequiredService<IMediator>(); // Resolve Program class
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
    mediator.Send(new DisplayWallCommand(userName)).Wait();
  }
  else
  {
    var projectName = parts[0];
    mediator.Send(new ReadProjectCommand(projectName)).Wait();
  }
}


public record PostMessageCommand(string UserName, string ProjectName, string Message) : IRequest<Unit>;
public record ReadProjectCommand(string ProjectName) : IRequest<Unit>;
public record FollowProjectCommand(string UserName, string ProjectName) : IRequest<Unit>;
public record DisplayWallCommand(string UserName) : IRequest<Unit>;

