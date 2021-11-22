
export class SurveyQuestion {
    Question: string;
    Section: number;
    Answer: number;
    
    constructor(question: string, section: number, answer: number)
    {
        this.Question = question;
        this.Section = section;
        this.Answer = answer;
    }
}