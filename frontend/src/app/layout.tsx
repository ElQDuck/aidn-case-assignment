import type { Metadata } from 'next'
import './globals.css'

export const metadata: Metadata = {
  title: 'NEWS Score Calculator',
  description: 'National Early Warning Score calculator',
}

export default function RootLayout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
    <html lang="en">
      <body>{children}</body>
    </html>
  )
}