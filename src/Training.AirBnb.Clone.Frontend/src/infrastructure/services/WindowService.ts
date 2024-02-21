import type {UrlNavigationOptions} from "@/infrastructure/services/UrlNavigationOptions";

export  class WindowService{
    public  navigateToUrl(url: string, navigationOptions: UrlNavigationOptions){
        window.open(url, navigationOptions);
    }

}