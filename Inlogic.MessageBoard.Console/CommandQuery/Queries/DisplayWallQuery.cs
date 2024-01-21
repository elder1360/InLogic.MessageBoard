using MediatR;

namespace Inlogic.MessageBoard.Console.CommandQuery.Queries;
public record DisplayWallQuery(string UserName) : IRequest;

