import { Moment } from "moment";

export interface IActivity {
  ActivityId: string;
  Title: string;
  Description: string;
  City: string;
  Venue: string;
  HoldingDate: Date;
  CategoryId: Int32Array;
  ImageFile: Blob;
}

export interface IActivityItem extends Partial<IActivity> {
  ImagePath: string;
  HostName: string;
  IsActive: boolean;
  CategoryName: string;
  MonthDiff: number;
  HoldingTime: Moment;
  HoldingDateDisplay: Moment;
}

export interface IACtivityEnvelope {
  TotalCount: number;
  ActivityItems: IActivityItem[];
}
export interface IOptionData {
  Key: string;
  Value: string;
  Label: string;
}
