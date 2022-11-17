import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AccountPageComponent } from './account-page/account-page.component';
import { RaidComponent } from './raid/raid.component';
import { MainPageComponent } from './main-page/main-page.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { AdminComponent } from './admin/admin.component';
import { TransactionComponent } from './transaction/transaction.component';

const routes: Routes = [
  //set route to login component for <route-outlet>
  {path: '', redirectTo: '/home', pathMatch: 'full'},
  {path: 'home', component: MainPageComponent},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'raid', component: RaidComponent},
  {path: 'account', component: AccountPageComponent},
  {path: 'userprofile', component: UserProfileComponent},
  {path: 'adminpanel', component: AdminComponent},
  {path: 'transactions', component: TransactionComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
