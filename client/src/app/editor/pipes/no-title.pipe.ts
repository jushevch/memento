import { Pipe, PipeTransform, Injectable } from "@angular/core";
import { EditorServicesModule } from "../editor-services.module";

@Pipe({
    name: "noTitle"
})
@Injectable({
    providedIn: EditorServicesModule
})
export class NoTitlePipe implements PipeTransform
{
    transform(title: string): string
    {
        return title ? title : "# # #";
    }
}
