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

export interface OrderReferenceBodyModel{
  orderReference: string
}

export interface CaptureRequest{
  paymentId: string,
  orderReference: string
}

export interface VoidModel {
  id: string,
  status: number
}

export interface  CaptureModel{
  id: string,
  status: number
}
