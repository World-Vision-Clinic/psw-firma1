export class Feedback {
    Content: string;
    isPublic: boolean;
    isAnonymous: boolean;
    
    constructor(Content: string, isPublic: boolean, isAnonymous: boolean)
    {
        this.Content = Content;
        this.isPublic = isPublic;
        this.isAnonymous = isAnonymous;
    }
}