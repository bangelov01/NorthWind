import axios, { type AxiosInstance } from 'axios'
import type { ProblemDetails } from '../types';

const apiClient: AxiosInstance = axios.create({
  baseURL: 'https://localhost:7097/api',
  headers: {
    'Content-Type': 'application/json',
    "Accept": 'application/json',
  },
  timeout: 10_000,
})

apiClient.interceptors.response.use(
  response => response,
  error => {
    if (axios.isAxiosError(error) && error.response) {
      const problem = error.response.data as ProblemDetails;
      const message = problem.detail ?? problem.title ?? 'An unexpected error occurred';
      console.error(`[API ${error.response.status}]:`, message);
      return Promise.reject(new Error(message));
    }
    return Promise.reject(error);
  }
);

export default apiClient
