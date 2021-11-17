import { DatePipe } from "@angular/common";

export class Feedback {
    Content: string;
    isPublic: boolean;
    isPublishable: boolean;
    isAnonymous: boolean;
    Date: Date = new Date();
    UserName: string = "";
    
    constructor(Content: string, isPublic: boolean, isPublishable: boolean, isAnonymous: boolean, UserName: string)
    {
        this.Content = Content;
        this.isPublic = isPublic;
        this.isPublishable = isPublishable;
        this.isAnonymous = isAnonymous;
        this.UserName = UserName;
    }
}