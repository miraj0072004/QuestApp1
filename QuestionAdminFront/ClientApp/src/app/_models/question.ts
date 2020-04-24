export interface Question
{
    id: number;
    questionText: string;
    questionCategory: string;
    answerOne: string;
    answerTwo: string;
    answerThree: string;
    correctAnswerIndex: number;
    answerExplanation: string;
}