using MediatR;

namespace Inlogic.MessageBoard.Console.CommandQuery.Commands;

public record FollowProjectCommand(string UserName, string ProjectName) : IRequest;

