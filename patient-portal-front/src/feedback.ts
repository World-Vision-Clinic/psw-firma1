export class Feedback {
    Id: number;
    Content: string;
    isPublic: boolean;
    isAnonymous: boolean;
    
    constructor(Id: number, Content: string, isPublic: boolean, isAnonymous: boolean)
    {
        this.Id = Id;
        this.Content = Content;
        this.isPublic = isPublic;
        this.isAnonymous = isAnonymous;
    }
}