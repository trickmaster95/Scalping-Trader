# 🤖 Simple Trading Bot (Demo)

This is a simple trading bot built to demonstrate a basic algorithmic trading strategy using common technical indicators. The bot is designed for educational purposes only and is **not intended for real or live trading**.

---

## 📌 Features

- **Strategy Based on Indicators:**
  - RSI (Relative Strength Index)
  - Moving Averages: MA5, MA25, MA99
- **Market Behavior Handling:**
  - Acts differently depending on whether the market is rising, falling, or stable
- **Instant Buy Execution:** 
  - Buys are placed immediately when conditions are met
- **OCO Orders:**
  - After a successful buy, an OCO (One-Cancels-the-Other) order is placed for take profit & stop loss
- **Trailing Feature:**
  - If enabled, the bot continuously observes the market
  - Cancels old orders and places updated ones to follow price movement

---

## ⚙️ How It Works

1. The bot continuously monitors the market using the defined indicators.
2. When entry conditions are met, it places a **market buy order**.
3. Immediately after the buy is filled, it sets up an **OCO order**.
4. If **trailing** is activated, it watches the market and:
   - Cancels the old OCO order
   - Replaces it with a new one reflecting updated price action

---

## 📁 Project Purpose

This project is meant to showcase how a simple trading logic can be structured and automated using code.  
It is useful for:

- Developers learning algorithmic trading concepts
- Practicing trading bot structure
- Demonstrating real-time strategy reaction

> ⚠️ **Disclaimer:** This bot is not intended for real-world financial use. It should only be used for learning and experimentation.

---

## 🌍 بالعربي

بوت تداول بسيط تم تطويره بهدف توضيحي وتعليمي، مبني على استراتيجية تعتمد على مؤشرات RSI و MA (5، 25، 99).  
البوت بيتصرف بشكل مختلف حسب حالة السوق (صاعد - هابط - مستقر)، وبينفذ أوامر شراء بشكل لحظي، وبعدها بيحط أمر OCO.  
ولو خاصية التريلينج مفعّلة، بيبدأ يراقب السوق ويحدّث الأوامر باستمرار حسب حركة السعر.

> ⚠️ هذا المشروع لأغراض تعليمية فقط وليس مخصصًا للتداول الحقيقي.

---

## 🚫 Usage Notice

This code is provided for **demonstration and personal learning only**.  
**You are not allowed to use, modify, distribute, or reuse any part of this project without explicit permission.**
