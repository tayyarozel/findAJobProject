import { ResponseModel } from "./responseModel";
//TEK BİR DATA İÇİN
export interface SingleResponseModel<T> extends ResponseModel{
  data:T
}
