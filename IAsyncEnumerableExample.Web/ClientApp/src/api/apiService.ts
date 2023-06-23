import axios from 'axios';
import { CanceledError } from 'axios';
import { basePath, currentMode } from '@/utils/baseUrl';
import { FetchFactory } from '@/api/fetchFactory';
import { ProductAPI } from '@/api/productApi';

const basePathApi = `${basePath}api`;
if (currentMode !== 'PRD') {
   console.log(`Current API path: ${basePathApi}`);
}

//Axios for classic requests
const httpRequest = axios.create({
   baseURL: basePathApi,
   withCredentials: true,
   headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json',
      'Access-Control-Allow-Credentials': true,
   },
});

//Modernized fetch for async enumerable requests
const asyncEnumerableRequest = new FetchFactory(basePathApi);

const productApi = new ProductAPI(httpRequest, asyncEnumerableRequest);

class API {
   product = productApi;
}

const isAxiosError = axios.isAxiosError;
export function isCanceledError(error: unknown): boolean {
   if (error instanceof CanceledError) {
      const retval = error && error?.name == 'CanceledError' && error?.code == 'ERR_CANCELED';
      return retval;
   }
   return false;
}

const APIStatement = new API();
export { APIStatement as API, isAxiosError };
