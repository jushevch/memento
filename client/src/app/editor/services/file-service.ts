import { Injectable } from "@angular/core";
import { EditorServicesModule } from "../editor-services.module";
import { StorageArea } from "../models/storage-area.model";
import { Area } from "../models/area.model";

@Injectable({
    providedIn: EditorServicesModule
})
export class FileService
{
    saveArea(area: Area): void
    {
        let file = new StorageArea(area);
        let json = JSON.stringify(file, null, 2);
        let blob = new Blob([json], { type: "text/plain" });
        let link = document.createElement("a");

        link.download = "area.json";
        link.href = URL.createObjectURL(blob);
        link.click();

        URL.revokeObjectURL(link.href);
    }

    loadArea(blob: Blob, area: Area): void
    {
        let reader = new FileReader();

        reader.onload = (ev) =>
        {
            let json = ev.target.result.toString();
            let file = JSON.parse(json) as StorageArea;

            area.initialize(file);
        };

        reader.onerror = (ev) =>
        {
            console.error(ev.target.error);
        };

        reader.readAsText(blob);
    }
}
