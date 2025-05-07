#  *Real-Time Strategy* (RTS) 🎮

Proyek game *Real-Time Strategy* (RTS) yang dikembangkan menggunakan Unity untuk Mata Kuliah `IFB-312 Pemrograman Game`.

## 🧾 Informasi Umum

- **Nama Proyek:** RTS_game
- **Versi Unity:** 6000.0.43f1
- **Render Pipeline:** Universal Render Pipeline (URP)
- **Target Platform:** Windows

## 🔧 Fitur

- Sistem input modern dengan Unity Input System
- Navigasi AI dengan Pathfinding (NavMesh)
- Multiplayer support (Unity Multiplayer Center)
- Visual Scripting (Bolt) untuk logika non-koding
- Cutscene system dengan Timeline
- Sistem modular dan komponen berbasis Unity ECS-style

## 📦 Package yang Digunakan

Beberapa package penting dalam proyek ini:

| Package | Versi |
|--------|--------|
| Universal Render Pipeline | 17.0.4 |
| Input System | 1.13.1 |
| AI Navigation | 2.0.7 |
| Timeline | 1.8.7 |
| Visual Scripting | 1.9.5 |
| Multiplayer Center | 1.0.0 |

Untuk daftar lengkap, lihat file [`Packages/manifest.json`](Packages/manifest.json)

## 🚀 Cara Menjalankan

1. **Clone repository**:
    ```bash
    git clone https://github.com/nothappenhere/RTS-game-unity.git
    ```
2. **Buka dengan Unity Hub**, pastikan menggunakan versi yang sama (**6000.0.43f1**)
3. Tunggu Unity menyelesaikan import asset
4. Jalankan scene utama (`MainScene.unity`)

## 💡 Catatan Penting

- Pastikan pipeline URP sudah aktif. Jika tidak tampil dengan benar, cek `GraphicsSettings` dan `PipelineAsset`.
- Versi Unity 6000.x adalah bagian dari Unity LTS terbaru (atau Unity 2025+), jadi pastikan telah menginstalnya melalui Unity Hub.

## 🤝 Kontribusi

Kontribusi sangat diterima! Silakan buka **Issue** atau **Pull Request** jika ada ide, bug, atau perbaikan.

---

*Project ini dikembangkan untuk keperluan pembelajaran dan pengembangan game RTS modern.*

