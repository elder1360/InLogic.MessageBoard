using MediatR;

namespace Inlogic.MessageBoard.Console.CommandQuery.Queries;

public record ReadProjectQuery(string ProjectName) : IRequest;

