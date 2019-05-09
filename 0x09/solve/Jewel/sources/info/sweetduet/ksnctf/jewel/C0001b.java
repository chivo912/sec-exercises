package info.sweetduet.ksnctf.jewel;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

/* renamed from: info.sweetduet.ksnctf.jewel.b */
final class C0001b implements OnClickListener {
    /* renamed from: a */
    private /* synthetic */ JewelActivity f1a;

    C0001b(JewelActivity jewelActivity) {
        this.f1a = jewelActivity;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        this.f1a.finish();
    }
}
