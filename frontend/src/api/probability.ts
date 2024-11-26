import axios from 'axios';

const API_URL = 'http://localhost:5137';

export type Operation = 'combinedWith' | 'either';

export interface CalculationRequest {
  probA: string;
  probB: string;
  operation: Operation;
}

export interface CalculationResponse {
  result: number;
}

export const calculateProbability = async (data: CalculationRequest): Promise<CalculationResponse> => {
  console.log("Calling api")
  const response = await axios.post(`${API_URL}/calculate`, data);
  return response.data;
};