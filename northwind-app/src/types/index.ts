export interface OrderSummaryDto {
  orderId: number
  totalValue?: number
  productCount?: number
}

export interface CustomerOverviewDto {
  customerId: string
  companyName?: string | null
  orderCount?: number
}

export interface CustomerDetailsDto {
  customerId: string
  companyName: string
  contactName?: string | null
  contactTitle?: string | null
  address?: string | null
  city?: string | null
  region?: string | null
  postalCode?: string | null
  country?: string | null
  phone?: string | null
  fax?: string | null
  orders: OrderSummaryDto[]
}

export interface ProblemDetails {
  type?: string | null
  title?: string | null
  status?: number | null
  detail?: string | null
  instance?: string | null
}
