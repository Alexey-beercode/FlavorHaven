export interface PaymentDTO {
  id: string;
  userId: string;
  dishId: string;
  amount: number;
  isCanceled: boolean;
}
