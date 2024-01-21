using MediatR;

namespace Inlogic.MessageBoard.Console.CommandQuery.Commands;
public class FollowProjectCommandHandler(IInMemoryStateStore stateStore) : IRequestHandler<FollowProjectCommand>
{
  private readonly IInMemoryStateStore _stateStore = stateStore;

  public async Task Handle(FollowProjectCommand request, CancellationToken cancellationToken)
  {
    _stateStore.FollowProject(request.UserName, request.ProjectName);
    System.Console.WriteLine($"{request.UserName} is now following {request.ProjectName}");
    await Task.CompletedTask;
  }
}
