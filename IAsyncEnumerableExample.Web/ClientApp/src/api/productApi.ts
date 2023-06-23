import { Product } from '@/models/product';
import { AxiosInstance } from 'axios';
import { FetchFactory, EnableStreamReader } from '@/api/fetchFactory';

export class ProductAPI {
   constructor(httpRequest: AxiosInstance, asyncEnumerableRequest: FetchFactory) {
      this.httpRequest = httpRequest;
      this.asyncEnumerableRequest = asyncEnumerableRequest;
   }

   private httpRequest: AxiosInstance;
   private asyncEnumerableRequest: FetchFactory;

   async getProducts(onlyEnabled: boolean): Promise<Product[]> {
      const result = await this.httpRequest.get<Product[]>('product/GetList', {
         params: {},
      });
      return result.data;
   }
   async getProductStream(onlyEnabled: boolean, onReceive: (elem: Product[]) => void) {
      const params = new URLSearchParams();
      params.append('onlyEnabled', onlyEnabled.toString());
      const response = await this.asyncEnumerableRequest.fetchGet('product/GetAsyncStream', params);
      await EnableStreamReader<Product>(response, onReceive);
   }
}
