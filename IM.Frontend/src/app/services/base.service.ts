import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";

export abstract class BaseService<T> {
  protected constructor(
    protected httpClient: HttpClient
  ) { }

  getUrl(url: string) {
    return environment.baseUrl + url;
  }
}
