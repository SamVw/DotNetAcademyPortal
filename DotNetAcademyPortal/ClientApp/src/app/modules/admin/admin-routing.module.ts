import { Routes, RouterModule } from '@angular/router';

import { NgModule } from '@angular/core';
import { CustomersComponent } from './pages/customers/customers.component';
import { AuthGuard } from 'src/app/core/guards/auth.guard';
import { AdminGuard } from 'src/app/core/guards/admin.guard';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { CustomerDetailComponent } from './pages/customer-detail/customer-detail.component';

const routes: Routes = [
  { path: '', component: DashboardComponent, canActivate: [AuthGuard, AdminGuard]
    , children: [
      { path: 'customers', component: CustomersComponent },
      { path: 'customers/:id', component: CustomerDetailComponent },
      { path: '', redirectTo: 'customers', pathMatch: 'full' },
    ] },
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AdminRouterModule { }
