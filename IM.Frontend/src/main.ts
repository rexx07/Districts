import {enableProdMode, importProvidersFrom} from '@angular/core';
import {bootstrapApplication} from '@angular/platform-browser';

import {environment} from './environments/environment';
import {AppComponent, tokenGetter} from './app/app.component';
import {provideRouter} from '@angular/router';
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {AuthInterceptor} from "./app/core";
import {NgMultiSelectDropDownModule} from "ng-multiselect-dropdown";
import {JwtModule} from "@auth0/angular-jwt";
import {routes} from "./app/app.routes";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {ToastrModule} from "ngx-toastr";

if (environment.production) {
  enableProdMode();
}

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(routes),
    importProvidersFrom(
      BrowserAnimationsModule,
      HttpClientModule,
      NgMultiSelectDropDownModule.forRoot(),
      ToastrModule.forRoot({
        positionClass: "toast-bottom-right"
      }),
      JwtModule.forRoot({
        config: {
          tokenGetter: tokenGetter,
        }
      })
    ),
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
  ]
}).catch((error) => console.error(error));
