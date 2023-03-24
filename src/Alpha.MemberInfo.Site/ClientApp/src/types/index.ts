export interface Result {
  code: number;
  message: string;
  isSuccessful: boolean;
}

export interface SubMessagesResult extends Result {
  subMessages: string[];
}
