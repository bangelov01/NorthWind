import apiClient from './axiosInstance'
import type { CustomerOverviewDto, CustomerDetailsDto } from '../types'

export async function getCustomers(companyName?: string): Promise<CustomerOverviewDto[]> {
  const params = companyName ? { CompanyName: companyName } : undefined
  const resp = await apiClient.get<CustomerOverviewDto[]>('/Customers', { params })
  return resp.data
}

export async function getCustomerById(id: string): Promise<CustomerDetailsDto> {
  const resp = await apiClient.get<CustomerDetailsDto>(`/Customers/${encodeURIComponent(id)}`)
  return resp.data
}

export default {
  getCustomers,
  getCustomerById,
}
