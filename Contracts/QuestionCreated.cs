namespace Contracts;

public record QuestionCreated(string QuestionId,string Title, 
    string Context, DateTime Created,List<string> Tags);
    
    