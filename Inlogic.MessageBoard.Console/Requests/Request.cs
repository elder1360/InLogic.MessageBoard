using MediatR;

namespace Inlogic.MessageBoard.Console.Requests;
public record FollowProjectCommand(string UserName, string ProjectName) : IRequest;
public record DisplayWallQuery(string UserName) : IRequest;

