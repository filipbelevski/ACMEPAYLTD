export interface ListResponseDto<T>{
  list: T[],
  totalCount: number,
  currentPage: number,
  pageSize: number
}

export interface TransactionResponse{
  paymentId: string,
  amount: number,
  currency: string,
  cardholderNumber: string,
  createdOn: Date,
  holderName: string,
  orderReference: string,
  status: string
}

export interface VoidModel {
  paymentId: string,
  status: number
}

export interface  CaptureModel{
  paymentId: string,
  status: number
}
