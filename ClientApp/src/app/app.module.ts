import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { AdminComponent } from './components/admin/admin.component';
import { AuthInterceptor } from './interceptors/auth-interceptor';
import { NavbarComponent } from './components/navbar/navbar.component';
import { DropdownDirective } from './directives/dropdown.directive';
import { DropdownCheckboxlistComponent } from './controls/dropdown-checkboxlist/dropdown-checkboxlist.component';
import { PartnerComponent } from './components/admin/partner/partner.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    AdminComponent,
    NavbarComponent,
    DropdownDirective,
    DropdownCheckboxlistComponent,
    PartnerComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {
        path: '',
        component: NavbarComponent,
        children: [
        ]
      },
      { path: 'login', component: LoginComponent },
      {
        path: 'admin',
        component: AdminComponent,
        children: [
          { path: 'partner', component: PartnerComponent}
        ]
      },
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
