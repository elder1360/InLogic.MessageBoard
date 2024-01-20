using MediatR;

public class PostMessageHandler : IRequestHandler<PostMessageCommand, Unit>
{
  public Task<Unit> Handle(PostMessageCommand request, CancellationToken cancellationToken)
  {
    Console.WriteLine($"{request.UserName} posted a message to {request.ProjectName}: {request.Message}");
    return Task.FromResult(Unit.Value);
  }

  public Task<Unit> Handle(ReadProjectCommand request, CancellationToken cancellationToken)
  {
    Console.WriteLine($"{request}");
    return Task.FromResult(Unit.Value);
  }
  public Task<Unit> Handle(FollowProjectCommand request, CancellationToken cancellationToken)
  {
    Console.WriteLine($"{request.UserName}");
    return Task.FromResult(Unit.Value);
  }
  public Task<Unit> Handle(DisplayWallCommand request, CancellationToken cancellationToken)
  {
    Console.WriteLine($"{request.UserName}");
    return Task.FromResult(Unit.Value);
  }
}

