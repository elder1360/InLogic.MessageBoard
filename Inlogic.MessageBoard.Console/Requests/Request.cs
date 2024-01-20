using MediatR;

namespace Inlogic.MessageBoard.Console.Requests;
public record PostMessageCommand(string UserName, string ProjectName, string Message) : IRequest<Unit>;
public record ReadProjectQuery(string ProjectName) : IRequest<Unit>;
public record FollowProjectCommand(string UserName, string ProjectName) : IRequest<Unit>;
public record DisplayWallQuery(string UserName) : IRequest<Unit>;

