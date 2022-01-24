import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { Badge } from "./badge.directive";

@NgModule({
  declarations: [Badge],
  imports: [CommonModule],
  exports: [Badge]
})
export class BadgeModule {}
