'use client'

import { useState } from 'react'
import MeasurementField from './MeasurementField'
import ScoreResult from './ScoreResult'

interface FormValues {
  bodyTemperature: string
  heartrate: string
  respiratoryRate: string
}

interface FormErrors {
  bodyTemperature?: string
  heartrate?: string
  respiratoryRate?: string
}

const INITIAL_VALUES: FormValues = {
  bodyTemperature: '',
  heartrate: '',
  respiratoryRate: '',
}

function validate(vals: FormValues): FormErrors {
  const errs: FormErrors = {}
  if (vals.bodyTemperature === '') errs.bodyTemperature = 'Required'
  else if (isNaN(Number(vals.bodyTemperature))) errs.bodyTemperature = 'Must be a number'
  if (vals.heartrate === '') errs.heartrate = 'Required'
  else if (isNaN(Number(vals.heartrate))) errs.heartrate = 'Must be a number'
  if (vals.respiratoryRate === '') errs.respiratoryRate = 'Required'
  else if (isNaN(Number(vals.respiratoryRate))) errs.respiratoryRate = 'Must be a number'
  return errs
}

export default function NewsForm() {
  const [values, setValues] = useState<FormValues>(INITIAL_VALUES)
  const [errors, setErrors] = useState<FormErrors>({})
  const [score, setScore] = useState<number | null>(null)
  const [isLoading, setIsLoading] = useState(false)
  const [apiError, setApiError] = useState<string | null>(null)

  function handleChange(e: React.ChangeEvent<HTMLInputElement>) {
    const { name, value } = e.target
    setValues((prev) => ({ ...prev, [name]: value }))
    if (errors[name as keyof FormErrors]) {
      setErrors((prev) => ({ ...prev, [name]: undefined }))
    }
    setScore(null)
    setApiError(null)
  }

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault()
    const validationErrors = validate(values)
    if (Object.keys(validationErrors).length > 0) {
      setErrors(validationErrors)
      return
    }
    setIsLoading(true)
    setApiError(null)
    // removed setScore(null) from here
    try {
      const response = await fetch('/api/measurements', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          measurements: [
            { type: 'TEMP', value: Number(values.bodyTemperature) },
            { type: 'HR',   value: Number(values.heartrate) },
            { type: 'RR',   value: Number(values.respiratoryRate) },
          ],
        }),
      })
      const data = await response.json()
      if (!response.ok) {
        setApiError(data.error ?? 'Something went wrong.')
        return
      }
      setScore(data.score)
    } catch {
      setApiError('Network error. Please try again.')
    } finally {
      setIsLoading(false)
    }
  }

  function handleReset() {
    setValues(INITIAL_VALUES)
    setErrors({})
    setScore(null)
    setApiError(null)
  }

  return (
    <div style={{ minHeight: '100vh', display: 'flex', alignItems: 'flex-start', justifyContent: 'center', paddingTop: '64px', backgroundColor: '#f9fafb' }}>
      <form
        onSubmit={handleSubmit}
        noValidate
        style={{ width: '404px', display: 'flex', flexDirection: 'column', gap: '40px' }}
      >
        <h1 style={{ margin: 0, fontFamily: 'Inter', fontWeight: 600, fontSize: '20px', lineHeight: '130%', color: '#000' }}>
          NEWS score calculator
        </h1>

        <div style={{ display: 'flex', flexDirection: 'column', gap: '40px' }}>
          <MeasurementField label="Body temperature" hint="Degrees celcius"   name="bodyTemperature" value={values.bodyTemperature} error={errors.bodyTemperature} onChange={handleChange} />
          <MeasurementField label="Heartrate"        hint="Beats per minute"  name="heartrate"       value={values.heartrate}       error={errors.heartrate}       onChange={handleChange} />
          <MeasurementField label="Respiratory rate" hint="Breaths per minute" name="respiratoryRate" value={values.respiratoryRate} error={errors.respiratoryRate} onChange={handleChange} />
        </div>

        <div style={{ width: '340px', height: '40px', display: 'flex', alignItems: 'center', gap: '24px' }}>
          <button
            type="submit"
            disabled={isLoading}
            style={{
              height: '40px',
              width: '201px',
              paddingTop: '8px',
              paddingBottom: '8px',
              paddingLeft: '16px',
              paddingRight: '16px',
              backgroundColor: '#7424DA',
              color: '#fff',
              border: 'none',
              borderRadius: '40px',
              fontFamily: 'Inter',
              fontWeight: 500,
              fontSize: '16px',
              lineHeight: '24px',
              cursor: isLoading ? 'not-allowed' : 'pointer',
              opacity: isLoading ? 0.6 : 1,
              whiteSpace: 'nowrap',
            }}
          >
            Calculate NEWS score
          </button>
          <button
            type="button"
            onClick={handleReset}
            style={{ 
              height: '40px',
              width: '115px',
              paddingTop: '8px',
              paddingBottom: '8px',
              paddingLeft: '16px',
              paddingRight: '16px',
              backgroundColor: '#FAF6FF',
              border: 'none',
              fontFamily: 'Inter',
              fontSize: '16px',
              lineHeight: '24px',
              color: '#000000',
              cursor: 'pointer' }}
          >
            Reset form
          </button>
        </div>

        {apiError && (
          <p style={{ margin: 0, fontSize: '14px', color: '#ef4444' }}>{apiError}</p>
        )}

        {score !== null && <ScoreResult score={score} />}
      </form>
    </div>
  )
}