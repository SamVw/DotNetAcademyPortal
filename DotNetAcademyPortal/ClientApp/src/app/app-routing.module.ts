import { Routes, RouterModule, PreloadAllModules } from '@angular/router';

import { NgModule } from '@angular/core';
import { AuthGuard } from './core/guards/auth.guard';
import { AdminGuard } from './core/guards/admin.guard';

const appRoutes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', loadChildren: () => import('./modules/login/login.module').then(mod => mod.LoginModule) },
  { path: 'admin', loadChildren: () => import('./modules/admin/admin.module').then(mod => mod.AdminModule) },
];

@NgModule({
  imports: [
    RouterModule.forRoot(
      appRoutes,
      {
        enableTracing: true, // <-- debugging purposes only
        preloadingStrategy: PreloadAllModules
      }
    )
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule {}
