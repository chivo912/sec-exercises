package info.sweetduet.ksnctf.jewel;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

/* renamed from: info.sweetduet.ksnctf.jewel.a */
final class C0000a implements OnClickListener {
    /* renamed from: a */
    private /* synthetic */ JewelActivity f0a;

    C0000a(JewelActivity jewelActivity) {
        this.f0a = jewelActivity;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        this.f0a.finish();
    }
}
