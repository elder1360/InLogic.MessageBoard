using MediatR;

namespace Inlogic.MessageBoard.Console.CommandQuery.Commands;

public record PostMessageCommand(string UserName, string ProjectName, string Message) : IRequest;

