export interface Result {
  code: number;
  message: string;
  isSuccessful: boolean;
  ErrMsg?: string;
}

export interface AvailableAlphaDomain extends Result {
  availableAlphaDomain: string[];
}

export interface SubMessagesResult extends AvailableAlphaDomain {
  subMessages: {
    isLive: boolean;
    message: string;
  }[];
}
